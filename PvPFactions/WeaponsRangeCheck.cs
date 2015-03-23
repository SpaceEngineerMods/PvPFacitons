using Sandbox.ModAPI;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.Components;
using System;
using System.Collections.Generic;
using System.Text;
<<<<<<< HEAD
using Sandbox.ModAPI.Ingame;

namespace WeaponsRangeCheck
=======

<<<<<<< HEAD:WeaponsRangeCheck.cs
using Sandbox.ModAPI.Ingame;

namespace WeaponsRangeCheck
=======
namespace ConsoleApplication2
>>>>>>> c04a03317a2fcba94cf23c648edab11be5289963
>>>>>>> 6600df22f67afe7516bf4f36e7171bd4dd1bcd94:PvPFactions/WeaponsRangeCheck.cs
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_Beacon))]
    public class MyBeaconLogic : MyGameLogicComponent
    {
        Sandbox.Common.ObjectBuilders.MyObjectBuilder_EntityBase m_objectBuilder = null;
        private bool m_greeted;
        public override void Close()
        {
        }

        public override void Init(Sandbox.Common.ObjectBuilders.MyObjectBuilder_EntityBase objectBuilder)
        {
            Entity.NeedsUpdate |= MyEntityUpdateEnum.EACH_100TH_FRAME;
            m_objectBuilder = objectBuilder;
        }

        public override void MarkForClose()
        {
        }

        public override void UpdateAfterSimulation()
        {
        }

        public override void UpdateAfterSimulation10()
        {
        }

        public override void UpdateAfterSimulation100()
        {
        }

        public override void UpdateBeforeSimulation()
        {
        }

        public override void UpdateBeforeSimulation10()
        {
        }

        public override void UpdateBeforeSimulation100()
        {



<<<<<<< HEAD:WeaponsRangeCheck.cs
            HashSet<IMyEntity> ships = new HashSet<IMyEntity>();
            //find all of the ships
            Sandbox.ModAPI.MyAPIGateway.Entities.GetEntities(ships, (x) => x is Sandbox.ModAPI.IMyCubeGrid);
            int i = 0;
            var GatlingGuns = new List<IMySmallGatlingGun>();
            // go through ships
            foreach (var ship in ships)
            {

                var templist = new List<Sandbox.ModAPI.IMySlimBlock>();
                // find all gatling guns in the group
                (ship as Sandbox.ModAPI.IMyCubeGrid).GetBlocks(templist,
                    x => x.FatBlock is IMySmallGatlingGun);

                foreach (var temp in templist)
                {
=======
            HashSet<IMyEntity> workingSmallGatlingGuns = new HashSet<IMyEntity>();
<<<<<<< HEAD
            HashSet<IMyEntity> IMySmallGatlingGun;
            Sandbox.ModAPI.MyAPIGateway.Entities.GetEntities(IMySmallGatlingGun,
                (x) => x is IMySmallGatlingGun && x.IsWorking);
=======
            Sandbox.ModAPI.MyAPIGateway.Entities.GetEntities(workingSmallGatlingGuns, (x) => x is IMySmallGatlingGun() && x.IsWorking);
>>>>>>> c04a03317a2fcba94cf23c648edab11be5289963
>>>>>>> 6600df22f67afe7516bf4f36e7171bd4dd1bcd94:PvPFactions/WeaponsRangeCheck.cs

                    GatlingGuns.Add(temp.FatBlock as IMySmallGatlingGun);
                }
            }

            foreach (var GatlingGun in GatlingGuns)
            {
<<<<<<< HEAD:WeaponsRangeCheck.cs
                i++;
                if (((Entity.GetTopMostParent().EntityId != GatlingGun.GetTopMostParent().EntityId)) && (GatlingGun.Enabled) && (GatlingGun.GetPosition() - Entity.GetPosition()).Length() < 20)
=======
<<<<<<< HEAD
                if (((Block.GetTopMostParent().EntityId() != SmallGatlingGun.GetTopMostParent().EntityId())) && (MyAPIGateway.Session.Player.GetPosition() - Entity.GetPosition()).Length() < 20)
=======
                if (((Block.GetTopMostParent().entityid() != SmallGatlingGun.GetTopMostParent().entityid())) && (MyAPIGateway.Session.Player.GetPosition() - Entity.GetPosition()).Length() < 20)
>>>>>>> c04a03317a2fcba94cf23c648edab11be5289963
>>>>>>> 6600df22f67afe7516bf4f36e7171bd4dd1bcd94:PvPFactions/WeaponsRangeCheck.cs
                {
                    if (!m_greeted)
                    { 
                        MyAPIGateway.Utilities.ShowNotification(i + " GatlingGuns Detected In Map", 1000, MyFontEnum.BuildInfoHighlight);
                        m_greeted = true;

                    }
                }
                else
                    m_greeted = false;
            }
        
        }

        public override void UpdateOnceBeforeFrame()
        {
        }

        public override MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false)
        {
            return m_objectBuilder;
        }
    }
<<<<<<< HEAD

    public class Block
    {
        public static void GetTopMostParent()
        {
            throw new NotImplementedException();
        }
    }
=======
>>>>>>> c04a03317a2fcba94cf23c648edab11be5289963
}