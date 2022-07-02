using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HovyFridgeApi.Migrations
{
    public partial class FunctionForSearchSuggestons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            /*
             
                CREATE OR REPLACE FUNCTION search_suggestions_func (stringQuery text) RETURNS SETOF public."ProductSuggestion" 
                LANGUAGE SQL
                AS $$
	                SELECT * FROM public."ProductSuggestion" WHERE "Name" LIKE '%' || stringQuery || '%';
                $$;
             
             */

            migrationBuilder.Sql(
                "CREATE OR REPLACE FUNCTION search_suggestions_func (stringQuery text) RETURNS SETOF public.\"ProductSuggestion\"" +
                "LANGUAGE SQL" +
                "AS $$" +
                    "SELECT * FROM public.\"ProductSuggestion\" WHERE \"Name\" LIKE '%' || stringQuery || '%';" +
                "$$;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION search_suggestions_func(text)");
        } 
    }
}
