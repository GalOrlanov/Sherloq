
select * from table22 where (field36 like '17.%' or field36 like '15.%') and table22.field38 in (select field38 from table3 where table22.year=table3.year and table22.field38=table3.field38 and field22 ='לא נמצאה סיבה מתאימה עבור תוצאת חישוב זכאות, יש להבין מה גרם לתוצאת החישוב הזו');