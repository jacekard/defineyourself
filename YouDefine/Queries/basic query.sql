select * from dbo.Ideas
inner join dbo.Definitions on dbo.Definitions.IdeaId = dbo.Ideas.IdeaId

delete from dbo.Ideas