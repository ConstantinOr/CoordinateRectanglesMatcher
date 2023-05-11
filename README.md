# CoordinateRectanglesMatcher
## Description
The project was created using .Net 6 and C#10.
API contains three endpoints:
1. ***rectangle/v1/seed*** GET For populate 200 rectangles in database.
2. ***rectangle/v1/search*** POST Need authentication. For searching rectangles that contained our points.
3. ***rectangle/v1/search*** POST Used for authentication end users.

By default hardcoded two users:
<p>{ "test1", "password1" },
{ "test2", "password2" }
</p>
You can use them for authentication.
By default used the "bearer" token.