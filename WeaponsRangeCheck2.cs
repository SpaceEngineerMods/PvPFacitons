using Sandbox.ModAPI;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.Components;
using System;
using System.Collections.Generic;
using System.Text;

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
            Sandbox.ModAPI.MyAPIGateway.Entities.GetEntities(ships, (x) => x is Sandbox.ModAPI.IMyCubeGrid) ;
            int i =0;
            var turrets = new List<IMyLargeMissileTurret>();
            // go through ships
            foreach (var ship in ships)
            {
                var templist = new List<Sandbox.ModAPI.IMySlimBlock>();
                // find all missile turrets in the group
                (ship as Sandbox.ModAPI.IMyCubeGrid).GetBlocks(templist,
                    x => x is IMyLargeMissileTurret && x.FatBlock.IsWorking && !x.IsDestroyed);

                foreach (var temp in templist)
                {
                    turrets.Add(temp.FatBlock as IMyLargeMissileTurret);
                }
            }
            
            foreach (var Missileturret in turrets)
            {   
                i++;
                if (((Entity.GetTopMostParent().EntityId != Missile.GetTopMostParent().EntityId)) && (Missile.GetPosition() - Entity.GetPosition()).Length() < 20)
                {
                    if (!m_greeted)
                    {
                        MyAPIGateway.Utilities.ShowNotification(string.Format("Hello I am {0}", (Entity as Sandbox.ModAPI.Ingame.IMyTerminalBlock).DisplayNameText), 1000, MyFontEnum.Red);
                        m_greeted = true;
                    }
                }
                else
                    m_greeted = false;
            }
            MyAPIGateway.Utilities.ShowNotification(i + " Missile Turrets Detected In Map", 1000, MyFontEnum.Red);
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
