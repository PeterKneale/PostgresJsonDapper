
## indexes

### indexes created
Create indexes for use cases `ListCarsByOwner`, `ListLicencedPerson` and `ListUnlicensedPerson`
```postgresql
create index idx_car_owner_id on cars (((data->'Owner'->>'Id')::uuid));

create index idx_person_has_licence on persons (((data ->> 'HasLicence')::bool));
```

### index usage
Examine the index being used by running `explain analyze ...`

```postgresql
-- query using idx_car_owner_id index
explain analyze select data from cars where (data -> 'Owner' ->> 'Id')::uuid= 'f7c8bc0c-87d7-46d4-86f6-37db01e27ee3'::uuid;

Bitmap Heap Scan on cars  (cost=4.18..12.68 rows=4 width=32) (actual time=0.008..0.010 rows=0 loops=1)
  Recheck Cond: ((((data -> 'Owner'::text) ->> 'Id'::text))::uuid = 'f7c8bc0c-87d7-46d4-86f6-37db01e27ee3'::uuid)
  ->  Bitmap Index Scan on idx_car_owner_id  (cost=0.00..4.18 rows=4 width=0) (actual time=0.004..0.005 rows=0 loops=1)
        Index Cond: ((((data -> 'Owner'::text) ->> 'Id'::text))::uuid = 'f7c8bc0c-87d7-46d4-86f6-37db01e27ee3'::uuid)
Planning Time: 0.127 ms
Execution Time: 0.030 ms


-- query using idx_person_has_licence index
explain analyze select data from persons where (data ->> 'HasLicence')::bool = true;

Bitmap Heap Scan on persons  (cost=8.30..27.66 rows=535 width=32) (actual time=0.015..0.017 rows=1 loops=1)
  Filter: ((data ->> 'HasLicence'::text))::boolean
  Heap Blocks: exact=1
  ->  Bitmap Index Scan on idx_person_has_licence  (cost=0.00..8.16 rows=535 width=0) (actual time=0.007..0.008 rows=1 loops=1)
        Index Cond: (((data ->> 'HasLicence'::text))::boolean = true)
Planning Time: 0.049 ms
Execution Time: 0.035 ms
```

## other queries
```postgresql
select * from cars;
select data -> 'Owner' as owner_as_json from cars;
select data ->> 'Owner' as owner_as_string from cars;
select data -> 'Owner' ->> 'Id' as id from cars;
select (data -> 'Owner' ->> 'Id')::uuid as guid from cars;
```
