Azure CosmosDb
==============

Internally, Cosmos DB stores "items" in
"containers"[[2]](https://en.wikipedia.org/wiki/Cosmos_DB#cite_note-2), with
these 2 concepts being surfaced differently depending on the API used (these
would be "documents" in "collections" when using the MongoDB-compatible API, for
example). Containers are grouped in "databases", which are analogous to
namespaces above containers. Containers are schema-agnostic, which means that no
schema is enforced when adding items.

By default, every field in each item is automatically indexed, generally
providing good performance without tuning to specific query patterns. These
defaults can be modified by setting an indexing policy which can specify, for
each field, the index type and precision desired. Cosmos DB offers 2 types of
indexes:

-   Range,
    supporting [range](https://en.wikipedia.org/wiki/Range_query_(database)) and
    ORDER BY queries,

-   Spatial, supporting [spatial
    queries](https://en.wikipedia.org/wiki/Spatial_query) from points, polygons
    and line strings encoded in standard GeoJSON fragments.

Containers can also enforce unique key constraints to ensure data
integrity.[[3]](https://en.wikipedia.org/wiki/Cosmos_DB#cite_note-3)

Each Cosmos DB container exposes a change feed, which clients can subscribe to
in order to get notified of new items being added or updated in the
container.[[4]](https://en.wikipedia.org/wiki/Cosmos_DB#cite_note-4) Item
deletions are currently not exposed by the change feed. Changes are persisted by
Cosmos DB, which makes it possible to request changes from any point in time
since the creation of the container.

![C:\\Users\\DH347QV\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\13AF29D6.tmp](media/87527d23b2cd58f567d594bcd2d1108c.jpg)
