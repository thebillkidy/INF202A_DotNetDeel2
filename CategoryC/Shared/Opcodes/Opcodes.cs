﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Opcodes
{
    // 8 bits long, range 0 - 255
    public enum ClientMessage : byte
    {
        SMSG_DISCONNECT     = 0x00,
        SMSG_BROADCAST      = 0x01,
        SMSG_LOGIN          = 0x02,
        CMSG_BROADCAST      = 0x03,
        CMSG_LOGIN          = 0x04,
        CMSG_DISCONNECT     = 0x05,
        CMSG_LOGOUT         = 0x06,
    }

    public struct Opcode
    {
        public ClientMessage opcode { get; set; }
        public byte[] data { get; set; }
    }
}
