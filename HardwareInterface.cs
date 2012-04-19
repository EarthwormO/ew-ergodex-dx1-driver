using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;




namespace DX1Interface
{

    [StructLayout(LayoutKind.Sequential)]
    public struct DEV_BROADCAST_DEVICEHANDLE
    {
        public uint dbch_size;
        public uint dbch_devicetype;
        public uint dbch_reserved;

        public IntPtr dcbh_handle;
        public IntPtr dcbh_hdevnotify;

        public Guid dcbh_eventguid;
        public UInt32 dcbh_nameoffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]        // Currently all out packets are 6 bytes Max
        public Byte[] dbch_data;
    }




    public class HardwareInterface
    {
        const int DIGCF_DEFAULT          = 0x00000001;  // only valid with DIGCF_DEVICEINTERFACE
        const int DIGCF_PRESENT          = 0x00000002;
        const int DIGCF_ALLCLASSES       = 0x00000004;
        const int DIGCF_PROFILE          = 0x00000008;
        const int DIGCF_DEVICEINTERFACE  = 0x00000010;

        const int kNumOutstandingReadRequests = 16;
        const int kSizeOfReadRequest = 8;


        public bool TestMode(IntPtr Devhandle)
        {
            if (Devhandle == (IntPtr)(-1))
                return false;

            // device, Write, command, buffered
            UInt32 controlCode = 0x65500 << 16 | 2 << 14 | 0x810 << 2 | 0;
            uint bytesReturned = 0;
            return DeviceIoControl(Devhandle, controlCode, IntPtr.Zero, 0, IntPtr.Zero, 0, ref bytesReturned, IntPtr.Zero);
        }

 
        public bool SendProgramPacket(IntPtr Devhandle, Byte[] mapping)
        {
            if (Devhandle == (IntPtr)(-1))
                return false;

            bool retVal;
            // device, Write, command, buffered
            UInt32 controlCode = 0x65500 << 16 | 2 << 14 | 0x801 << 2 | 0;
            Byte[] commandPacket = { 0x02, 0x01, 0x06, 0x01, 0x03, 0x00, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x01, 0x07 };

            if (mapping == null)
                mapping = commandPacket;

            uint bytesReturned = 0;
            retVal = DeviceIoControl(Devhandle, controlCode, mapping, mapping.Length, IntPtr.Zero, 0, ref bytesReturned, IntPtr.Zero);

            return retVal;
        }

        public bool RegisterWindowForEvents(IntPtr DevHandle, IntPtr formHandle)
        {
            // And register for the actual event
            DEV_BROADCAST_DEVICEHANDLE devHandle = new DEV_BROADCAST_DEVICEHANDLE();
            devHandle.dbch_size = (uint)Marshal.SizeOf(devHandle);
            devHandle.dbch_devicetype = 6;
            devHandle.dcbh_handle = DevHandle;
            devHandle.dcbh_hdevnotify = IntPtr.Zero;
            devHandle.dcbh_eventguid = new Guid("573E8C73-0CB4-4471-A1BF-FAB26C31D384");

            IntPtr not2 = RegisterDeviceNotification(formHandle, ref devHandle, 0);

            return not2 != IntPtr.Zero;
        }


        public IntPtr OpenDevice(bool synchronous)
        {
            string path = GetDevicePath();
            if (path == "")
                return (IntPtr)(-1);

            IntPtr Devhandle = CreateFile(path, FileAccess.GenericRead | FileAccess.GenericWrite, 
                                    FileShare.Write | FileShare.Read, 
                                    IntPtr.Zero,
                                    CreationDisposition.OpenExisting,
                                    ((synchronous ? FileAttributes.Normal : FileAttributes.Overlapped) | FileAttributes.SecurityImpersonation),
                                    IntPtr.Zero);


            return Devhandle;
        }


        public string GetDevicePath()
        {
            Guid deviceGuid = new Guid("573E8C73-0CB4-4471-A1BF-FAB26C31D384");
            IntPtr DeviceInfo = SetupDiGetClassDevs(ref deviceGuid, IntPtr.Zero, IntPtr.Zero, (DIGCF_PRESENT | DIGCF_DEVICEINTERFACE));
            if (DeviceInfo == (IntPtr)(-1))
            {
                Console.WriteLine("SetupDiGetClassDevs Failed");
                return "";
            }

            SP_DEVICE_INTERFACE_DATA deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
            deviceInterfaceData.cbSize = Marshal.SizeOf(deviceInterfaceData);

            bool result = SetupDiEnumDeviceInterfaces(DeviceInfo, IntPtr.Zero, ref deviceGuid, 0, ref deviceInterfaceData);

            if (!result)
            {
                Console.WriteLine("SetupDiEnumDeviceInterfaces Failed");
                SetupDiDestroyDeviceInfoList(DeviceInfo);
                return "";
            }

            uint requiredSize = 0;


            SetupDiGetDeviceInterfaceDetail(DeviceInfo, ref deviceInterfaceData, IntPtr.Zero, 0, out requiredSize, IntPtr.Zero);
            SP_DEVICE_INTERFACE_DETAIL_DATA didd = new SP_DEVICE_INTERFACE_DETAIL_DATA();
            if (requiredSize < Marshal.SizeOf(didd))
            {
                didd.cbSize = 6;
                uint length = requiredSize;
                result = SetupDiGetDeviceInterfaceDetail(DeviceInfo, ref deviceInterfaceData, ref didd, length, out requiredSize, IntPtr.Zero);
            }

            Console.WriteLine("NameLength = " + didd.DevicePath.Length);
            Console.WriteLine("Name = " + didd.DevicePath);

            SetupDiDestroyDeviceInfoList(DeviceInfo);
            if (!result)
                return "\\\\?\\usb#vid_1603&pid_0002&mi_01#6&b8aa88a&0&0001#{573e8c73-0cb4-4471-a1bf-fab26c31d384}";

            return didd.DevicePath;
        }







        // Kernel32.dll

        [Flags]
        private enum FileAccess : uint
        {
            /// <summary>
            /// 
            /// </summary>
            GenericRead = 0x80000000,
            /// <summary>
            /// 
            /// </summary>
            GenericWrite = 0x40000000,
            /// <summary>
            /// 
            /// </summary>
            GenericExecute = 0x20000000,
            /// <summary>
            /// 
            /// </summary>
            GenericAll = 0x10000000
        }

        [Flags]
        private enum FileShare : uint
        {
            /// <summary>
            /// 
            /// </summary>
            None = 0x00000000,
            /// <summary>
            /// Enables subsequent open operations on an object to request read access. 
            /// Otherwise, other processes cannot open the object if they request read access. 
            /// If this flag is not specified, but the object has been opened for read access, the function fails.
            /// </summary>
            Read = 0x00000001,
            /// <summary>
            /// Enables subsequent open operations on an object to request write access. 
            /// Otherwise, other processes cannot open the object if they request write access. 
            /// If this flag is not specified, but the object has been opened for write access, the function fails.
            /// </summary>
            Write = 0x00000002,
            /// <summary>
            /// Enables subsequent open operations on an object to request delete access. 
            /// Otherwise, other processes cannot open the object if they request delete access.
            /// If this flag is not specified, but the object has been opened for delete access, the function fails.
            /// </summary>
            Delete = 0x00000004
        }

        private enum CreationDisposition : uint
        {
            /// <summary>
            /// Creates a new file. The function fails if a specified file exists.
            /// </summary>
            New = 1,
            /// <summary>
            /// Creates a new file, always. 
            /// If a file exists, the function overwrites the file, clears the existing attributes, combines the specified file attributes, 
            /// and flags with FILE_ATTRIBUTE_ARCHIVE, but does not set the security descriptor that the SECURITY_ATTRIBUTES structure specifies.
            /// </summary>
            CreateAlways = 2,
            /// <summary>
            /// Opens a file. The function fails if the file does not exist. 
            /// </summary>
            OpenExisting = 3,
            /// <summary>
            /// Opens a file, always. 
            /// If a file does not exist, the function creates a file as if dwCreationDisposition is CREATE_NEW.
            /// </summary>
            OpenAlways = 4,
            /// <summary>
            /// Opens a file and truncates it so that its size is 0 (zero) bytes. The function fails if the file does not exist.
            /// The calling process must open the file with the GENERIC_WRITE access right. 
            /// </summary>
            TruncateExisting = 5
        }

        [Flags]
        private enum FileAttributes : uint
        {
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotContentIndexed = 0x00002000,
            Encrypted = 0x00004000,
            SecurityImpersonation = 0x00020000,
            Write_Through = 0x80000000,
            Overlapped = 0x40000000,
            NoBuffering = 0x20000000,
            RandomAccess = 0x10000000,
            SequentialScan = 0x08000000,
            DeleteOnClose = 0x04000000,
            BackupSemantics = 0x02000000,
            PosixSemantics = 0x01000000,
            OpenReparsePoint = 0x00200000,
            OpenNoRecall = 0x00100000,
            FirstPipeInstance = 0x00080000

        }


        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(
           string fileName,
           [MarshalAs(UnmanagedType.U4)] FileAccess fileAccess,
           [MarshalAs(UnmanagedType.U4)] FileShare fileShare,
           IntPtr securityAttributes,
           [MarshalAs(UnmanagedType.U4)] CreationDisposition creationDisposition,
           FileAttributes flags,
           IntPtr template);


        [DllImport("Kernel32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        private static extern bool DeviceIoControl(
            IntPtr hDevice,
            UInt32 IoControlCode,
            [MarshalAs(UnmanagedType.AsAny)]
            [In] object InBuffer,
            int nInBufferSize,
            [In] IntPtr OutBuffer,
            uint nOutBufferSize,
            ref uint pBytesReturned,
            IntPtr Overlapped
            //            [In] ref System.Threading.NativeOverlapped Overlapped
        );


        [DllImport("Kernel32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        private static extern bool DeviceIoControl(
            IntPtr hDevice,
            UInt32 IoControlCode,
            [MarshalAs(UnmanagedType.AsAny)]
            [In] object InBuffer,
            uint nInBufferSize,
            [In] IntPtr OutBuffer,
            uint nOutBufferSize,
            ref uint pBytesReturned,
//            IntPtr Overlapped
            ref System.Threading.NativeOverlapped Overlapped
        );



        // SetupApi.dll

        [StructLayout(LayoutKind.Sequential)]
        private struct SP_DEVINFO_DATA
        {
            public uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SP_DEVICE_INTERFACE_DATA
        {
            public int cbSize;
            public System.Guid InterfaceClassGuid;
            public int Flags;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetupDiDestroyDeviceInfoList( 
           IntPtr hDevInfo
        );


        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetupDiGetClassDevs(           // 1st form using a ClassGUID
           ref Guid ClassGuid,
           IntPtr Enumerator,
           IntPtr hwndParent,
           int Flags
        );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiEnumDeviceInterfaces(
           IntPtr hDevInfo,
           IntPtr devInfo,
           ref Guid interfaceClassGuid,
           UInt32 memberIndex,
           ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData
        );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiGetDeviceInterfaceDetail(
           IntPtr hDevInfo,
           ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
           IntPtr deviceInterfaceDetailData,
           UInt32 deviceInterfaceDetailDataSize,
           out UInt32 requiredSize,
           IntPtr deviceInfoData
        );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiGetDeviceInterfaceDetail(
           IntPtr hDevInfo,
           ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
           ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
           UInt32 deviceInterfaceDetailDataSize,
           out UInt32 requiredSize,
           IntPtr deviceInfoData
        );


        [StructLayout(LayoutKind.Sequential)]
        private struct DEV_BROADCAST_DEVICEINTERFACE
        {
            public uint dbcc_size;
            public uint dbcc_devicetype;
            public uint dbcc_reserved;
            public Guid dbcc_classguid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string dbcc_name;
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, ref DEV_BROADCAST_DEVICEINTERFACE NotificationFilter, uint Flags);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, ref DEV_BROADCAST_DEVICEHANDLE NotificationFilter, uint Flags);


    }


}


