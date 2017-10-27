
function onCreate()
    OFP:displaySystemMessage("onCreate - in level")
end

function onMissionStart()
	local gametime = OFP:getMissionTime()
	OFP:displaySystemMessage(gametime)
	--[[
	OFP:displaySystemMessage("scripts is "..tostring(type(scripts))) --table

	OFP:displaySystemMessage("scripts.mission is "..tostring(type(scripts.mission))) --table
	OFP:displaySystemMessage("key in scripts.mission:")
	for k, _ in pairs(scripts.mission) do
		OFP:displaySystemMessage(k)
	end
	--]]
	--[[
	OFP:displaySystemMessage("scripts.mission.waypoints.simpleFunc is " --function
		..tostring(type(scripts.mission.waypoints.simpleFunc)))
	OFP:displaySystemMessage("scripts.mission.waypoints.simpleFunc: " --nil
		..tostring(scripts.mission.waypoints.simpleFunc))
	--]]
	scripts.mission.waypoints.simpleFunc()
end
