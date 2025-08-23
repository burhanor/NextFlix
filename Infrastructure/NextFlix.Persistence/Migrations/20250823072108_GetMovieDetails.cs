using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextFlix.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GetMovieDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE GetMovieDetails
	            @MovieId INT
                AS
                BEGIN

	            SET NOCOUNT ON;


	            SELECT Id,Title,Description,Duration,Status,UserId,CreatedDate,PublishDate,Poster,Slug from Movies WHERE Id = @MovieId;
                SELECT u.Id,u.Nickname,u.EmailAddress,u.UserType,u.Avatar,u.Slug,u.IsActive,u.CreatedDate from Users u INNER JOIN Movies m on m.UserId=u.Id  WHERE m.Id = @MovieId;
                
                SELECT t.Id,mt.DisplayOrder,T.Name,t.Slug,t.Status 
                FROM Movies m 
                INNER JOIN MovieTags mt on m.Id=mt.MovieId
                INNER JOIN Tags t on t.Id=mt.TagId
                WHERE m.Id = @MovieId;
                SELECT c.Id,mc.DisplayOrder,c.Name,c.Slug,c.Status
                FROM Movies m
                INNER JOIN MovieCategories mc on m.Id=mc.MovieId
                INNER JOIN Categories c on c.Id=mc.CategoryId
                WHERE m.Id = @MovieId;
                
                SELECT ch.Id,mc.DisplayOrder,ch.Name,ch.Slug,ch.Status,ch.Logo
                FROM Movies m
                INNER JOIN MovieChannels mc on m.Id=mc.MovieId
                INNER JOIN Channels ch on ch.Id=mc.ChannelId
                WHERE m.Id = @MovieId;
                
                SELECT c.Id,mc.DisplayOrder,c.Name,c.Slug,c.Status,c.Flag
                FROM Movies m
                INNER JOIN MovieCountries mc on m.Id=mc.MovieId
                INNER JOIN Countries c on c.Id=mc.CountryId
                WHERE m.Id = @MovieId;
                
                SELECT mc.DisplayOrder,c.Id,c.Name,c.Slug,c.Avatar,c.Status,c.BirthDate,c.Biography,c.CountryId,c.CastType,c.Gender 
                FROM Movies m
                INNER JOIN MovieCasts mc on m.Id=mc.MovieId
                INNER JOIN Casts c on c.Id=mc.CastId
                WHERE m.Id = @MovieId;
                SELECT mt.DisplayOrder,mt.TrailerLink 
                FROM MovieTrailers mt 
                WHERE mt.MovieId=@MovieId;
                
                SELECT ms.Link,ms.DisplayOrder,s.Id,s.Title,s.Status,s.SourceType 
                FROM Movies m
                INNER JOIN MovieSources ms on m.Id=ms.MovieId
                INNER JOIN Sources s on s.Id=ms.SourceId
                WHERE m.Id=@MovieId;
                
                SELECT   COUNT(*) AS ViewCount
                FROM 
                (
                	SELECT DISTINCT 
                	    IpAddress,
                	    CAST(ViewDate AS DATE) AS ViewDay
                	FROM MovieViews
                	WHERE MovieId = @MovieId
                ) AS uv;
                
                SELECT 
                    Vote,
                    COUNT(*) AS UniqueVoteCount
                FROM
                (
                    SELECT DISTINCT 
                        Vote,
                        IpAddress,
                        CAST(VoteDate AS DATE) AS VoteDay
                    FROM MovieLikes
                    WHERE MovieId = @MovieId
                ) AS uv
                GROUP BY Vote;
                
                
                END
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetMovieDetails");
		}
    }
}
