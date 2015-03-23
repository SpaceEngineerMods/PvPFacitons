using System;
using Sandbox.Common;
using Sandbox.Common.Components;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using VRageMath;

//Basic imports

namespace AutomatedAgressionResponse //teleporter namespace
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_CubeGrid))
