IMemoryCache: The in-memory cache can be used to store the links and their associated information. We generate a unique cache key for each user based on their username, and store the link's status (whether it's used or expired).
The links can expire after a certain amount of time, which is configurable through the ExpirationMinutes field in the API request.
When a user accesses the link, we can check the cache to see if the link has been used or has expired. If it hasn't, we display the secret message and mark the link as used.

havent implemented In memory cache as it was a strech goal but proposing my solution to question - The client needs to generate a large number of links each month (1 million+). Design a solution that
 meets the above requirements but minimizes the amount of data storage required
