﻿using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if SIGNED
[assembly: InternalsVisibleTo("NQuery.Tests, PublicKey=002400000480000094000000060200000024000052534131000400000100010031e49f37019bc9f4677811808b27379d59e75c23cc2249cc74a4df2b3215bc0a2bf2504f3023dfcdebb0445cddc4db97c32ae3906796989e5bd4d7b3105be3e5167962412ad1b674488438e72097db4e700e78396fa90f325b507b585202669313f2c77ea46002f6d017153fa548d3766b6faedb3caee5d34c20150492b897d7")]
#else
[assembly: InternalsVisibleTo("NQuery.Tests")]
#endif