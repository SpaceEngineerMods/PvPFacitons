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
    [MyEntityComponentDescriptor(typeof (MyObjectBuilder_Beacon))]
    public class MyBeaconLogic : MyGameLogicComponent
    {
        private Sandbox.Common.ObjectBuilders.MyObjectBuilder_EntityBase m_objectBuilder = null;
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

            HashSet<IMyEntity> infractingships = new HashSet<IMyEntity>();
            var station = Entity.GetTopMostParent();
            //find all of the ships
            var gatlingGuns = new HashSet<IMyEntity>();
            MyAPIGateway.Entities.GetEntities(gatlingGuns, (x) => x is IMySmallGatlingGun && (x.GetTopMostParent().GetPosition() - station.GetPosition()).Length() < 1000);

            
            // go through ships
         

            foreach (var gatlingGun in gatlingGuns.Select(gatling => gatling as IMySmallGatlingGun))
            {
                try
                {
                    if (Entity.GetTopMostParent().EntityId != gatlingGun.GetTopMostParent().EntityId &&
                        (gatlingGun.Enabled) &&
                        gatlingGun.OwnerId != (Entity as Sandbox.ModAPI.IMyTerminalBlock).OwnerId)
                        
                    {
                        infractingships.Add(gatlingGun.GetTopMostParent());

                    }


                }
                catch
                {
                    return;
                }
            }

            foreach (var ship in infractingships)
            {
                try
                {
                    var tempattackplayerid = (ship as Sandbox.ModAPI.IMyCubeGrid).BigOwners[0];
                    var tempdefendplayerid = (Entity as Sandbox.ModAPI.IMyTerminalBlock).OwnerId;
                    var tempattackfactionid = MyAPIGateway.Session.Factions.TryGetPlayerFaction(tempattackplayerid);
                    var tempdefendfactionid = MyAPIGateway.Session.Factions.TryGetPlayerFaction(tempdefendplayerid);

                    if (VRageMath.ContainmentType.Contains ==
                        ship.WorldAABB.Contains(MyAPIGateway.Session.Player.GetPosition()))
                    {
                        MyAPIGateway.Session.Factions.DeclareWar(tempdefendfactionid.FactionId,
                            tempattackfactionid.FactionId);
                        MyAPIGateway.Utilities.ShowNotification("You have failed to unpower your weapons", 1000,
                            MyFontEnum.BuildInfoHighlight);
                    }
                }
                catch 
                {
                    MyAPIGateway.Utilities.ShowNotification("We have detected you have no ownership data, you shall soon be detained.", 1000, MyFontEnum.BuildInfoHighlight);
                   
                }
                
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