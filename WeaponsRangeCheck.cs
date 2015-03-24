using Sandbox.ModAPI;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

using Sandbox.ModAPI.Ingame;

namespace WeaponsRangeCheck
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

                    GatlingGuns.Add(temp.FatBlock as IMySmallGatlingGun);
                }
            }
    
    
            foreach (var GatlingGun in GatlingGuns)
            {




                if (((Entity.GetTopMostParent().EntityId != GatlingGun.GetTopMostParent().EntityId)) && (GatlingGun.Enabled) && (GatlingGun.OwnerId != (Entity as Sandbox.ModAPI.IMyTerminalBlock).OwnerId) && (VRageMath.ContainmentType.Contains == GatlingGun.GetTopMostParent().WorldAABB.Contains(MyAPIGateway.Session.Player.GetPosition())) && (GatlingGun.GetPosition() - Entity.GetPosition()).Length() < 20)
                {

                    if (!m_greeted)
                    {
                        i++;
                        MyAPIGateway.Utilities.ShowNotification(i + " GatlingGuns Detected In Map", 1000, MyFontEnum.BuildInfoHighlight);
                        MyAPIGateway.Session.Factions.DeclareWar(factionBuilder.FactionId, selectedPlayer.PlayerId);
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
}