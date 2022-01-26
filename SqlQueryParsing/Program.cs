using SqlQueryParsing.DataAssets;
using System;

namespace SqlQueryParsing
{
    class Program
    {
        public static void Main()
        {
            //var query = @"select a.field38 from table1 a
            //             left join table22 t on t.field38 = a.field38 and t.year = 2020
            //             where end_year = 2019 and t.field12 = 0 group by a.field38";

            //var query = @"select * from table1";

            //            var query = @"select u.id, t.year from table22 t
            //left join table24 u on u.id = t.field38
            //LEFT JOIN table17 p ON p.field38 = u.id
            //where (COALESCE(p.ta_partner_type, ''::character varying)::text <> 'בן זוג משני'::text OR p.ta_partner_type::text = 'בן זוג משני'::text AND t.year < u.field20) and t.field36 = '22. לעדכן את המשתמש שהגיע חלק מהכסף' and field32 is true and field24 is true and field14 is not true and t.year < 2019 and NOT (EXISTS ( SELECT x.field1
            //       FROM ( SELECT table20.id,
            //         table20.field38,
            //         table20.key,
            //         table20.log_datetime,
            //         table20.field1,
            //         table20.client_id,
            //         table20.client_display_name,
            //         table20.source_ip,
            //         table20.temporary_key
            //         FROM table20
            //         WHERE table20.field38 = t.field38 AND table20.key ~~ (((('TA_OPERATION_FILES/להגשה '::text || t.year) || ' - '::text) || u.id_number::text) || '%'::text)
            //         ORDER BY table20.log_datetime DESC
            //         LIMIT 1) x
            //       WHERE x.field1::text = ANY (ARRAY['PUT'::character varying::text, 'RENAME_TO'::character varying::text, 'GRANT_GET'::character varying::text]))) and exists (select * from (select * from table6_view l where l.field38 = u.id and l.resource_id = t.id::text and l.new_value = '22. לעדכן את המשתמש שהגיע חלק מהכסף' order by l.log_datetime desc limit 1) x where x.client_display_name = 'ShaamApi')";

            var query = @"
select * from table22 where (field36 like '17.%' or field36 like '15.%') and table22.field38 in (select field38 from table3 where table22.year=table3.year and table22.field38=table3.field38 and field22 ='לא נמצאה סיבה מתאימה עבור תוצאת חישוב זכאות, יש להבין מה גרם לתוצאת החישוב הזו');"; 

            var parsedQuery = SqlQueryParser.ParseQuery(query);
            var listOfTables = parsedQuery.listOfTables;
            var listOfFields = parsedQuery.listOfFields;
            var ast = parsedQuery.queryAST;

            foreach (Table table in listOfTables)
            {
                Console.WriteLine(table.ToString());
            }

            Console.WriteLine();

            foreach (Field field in listOfFields)
            {
                Console.WriteLine(field.ToString());
            }

            Console.WriteLine();
            Console.WriteLine(ast.ToString());
        }
    }
}