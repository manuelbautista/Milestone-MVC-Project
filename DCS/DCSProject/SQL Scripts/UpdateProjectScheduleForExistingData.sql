Update B 
Set Complete=A.AvgComplete
FROM ProjectSchedule B
inner join (select avg(ISNULL(Complete,0)) AS AvgComplete,ProjectId from ProjectMilestones Group by ProjectId) AS A on A.ProjectId=B.ProjectId
