# gutenberg
Exam project Database and Test 2017
Report Results
Report:
Which database engines are used.
How data is modeled.
How data is imported.
Behavior of query test set.
Your recommendation, for which database engine to use in such a project for production.
Additional information and comments
Database engines
We’ve chosen to use MySQL & MongoDB.
MySQL since it’s easy to use and we are all already familiar with it. 
It’s also tends to have poor performance scaling with very large data sets, so it would be interesting to compare with.
MongoDB because it is supposed to work well with big data sets and is schema less so it has no major impact if one book’s documental structure is different from the next.
Data structure
The MySQL database is built up by 3 main tables, author, book and city, with 2 additional junction tables, book_author, which handles the book to author relation, and the book_city table which handles the book to city relation.
Each of the above tables is each having their own column’s, but they all have in common that they start with an id for their respective table.
The junction tables only hold foreign keys relating to the main tables to facilitate a many-to-many relationship between the tables. i.e. a book can have multiple authors but an author can also write multiple books.
 
For our mongodb database we used a different structure. Mongodb is schemaless and so we didn’t have to adhere to any rules of relations or jump through hoops if the structure changed along the way. Since mongodb uses Bsondocuments (like json) we are able to have nested arrays within a document.
 
We did however need a structure that was queryable, so we decided to accept redundancies in data where it made the most sense: cities and books.
Therefore our mongodb collection is a collection of “author” documents, which in turn has an array of books the author has written, which in turn has an array of which cities are mentioned within the book.
 
 
Data import
We made an extractor solution.
The extractor program reads through the designated book folder, containing all the books we would like to retrieve information from, and finds the rows containing title and author, creates an internal object with title and a reference to, if already existing, the author object in the memory “table”.
It creates the database dataset in its memory and runs through all the designated books, then it begins to insert the data into the MySQL database and then the MongoDB database.
 
 
 
Recommendation database
 
 
 
 
Additional information and comments on things that that have had high impact on production and/or execution of the project.
Test environment v. production environment
For development, querying against a very large dataset was unviable. Therefore we created a 2 of both of our databases; 1 for test and 1 for production. This meant that we could develop our application, while using a faster machine to extract data and populate with the big dataset.
When going from test to production we would then only have to change the connectionstring accordingly.
Map Limit
We chose to go with Google Static Map because it is a easy-use-easy-implementation tool that can be use through regular URL requests.
Google does however have certain limitations for it’s free-of-charge map plotting.
It is limited to 8192 characters per url request, where, in our case at least, we use around 157 characters for our google api key and hardcoded standard URL material, this gives us about 8000 characters to “play with” for adding city coordinations. After some testing we decided that 500 cities, per map, would be our hard limit due to several factors such as handling negative coordinates resulting in 2 additional characters in form of a minus(-), plus the other needed characters for Google’s specified URL format.
Books with no author
We noticed that, despite Project Gutenberg’s own documentation, quite many books came without an author, a blank field, “Author: “. We did not encounter this problem until quite late because we had not happen to have any books in our test collection that had this issue.
Various and Anonymous as author is not uncommon and we had already handled those to as being “unique” author names, but when we did encounter the problem of the missing author we took a decision to drop all books where an author were non-existing.
We took this decision because we will not have a book that does not correspond with any author, it does not follow the documentation we have looked through and it does not follow the format of the rest of the book collection that we have worked with doing this project.
The book would become a “dead end” in itself.
 
“Small” book collection
We chose to stop our download of Project Gutenberg “early”, because we had trouble with downloading the collection on a droplet, and therefore, had to use our own computers, which meant that we were limited in manners of computer usage over the duration of the 2 day download period, and that became an issue by the time we reached 35.000 books.
We then decided to continue working with this “limited” collection of books, 35.000, contrary to the full size of Project Gutenberg's 53.000 books.
We think that this is a sufficient number of books to illustrate that we are capable of handling and working with big datasets.
The main reason for this decision were the excessive amount of time needed to download the full collection.
No Auto_increment For MySQL
We have chosen not to use MySQL’s AI function to speed up performance of our BookExtractor, as it does not insert into the database for each and every book, city or author it encounters, but makes the data set in the host’s memory, in effect creating junction tables in memory so that we may keep pointing to already existing authors, books or cities.
Cutting out the many repeated SQL call’s saves us on performance cost pr. handled object, but it does however comes with the cost that our program has to run all the way through before we insert into our database.
No alternative city names
We felt that the Gutenberg city tables came with a lot of unrequired information in accordance to the requirements of the application. To better optimize our database structure we removed everything we deemed unnecessary, leaving us with id, name, ascii_name, latitude and longitude. Later consulting Helge we also removed alternative_names, because it added an unnecessary amount of complexity to something that could function without it. 
 
Comparing performance of the two database systems
After we switched to our production database to run our queries on a full scale dataset of ~35.000 books we discovered that our server was far too slow. We were running a DigitalOcean 1 core 2gb ram droplet.
So we resized to dualcore 4gb ram, and most of our queries finished in under 10 seconds. However 2 of our queries were still extremely lengthy. One in particular didn’t finish even after 9 minutes. This would have been solved (in all probability) by setting indexes for each table, but this operation took longer than we had.
So we made an executive decision; we’ll use a subset of 10000 book entries!
In no small part because we couldn’t afford a higher performance-level of droplet.

Conclusion: Generally Mysql is quite slow at handling large datasets. however, when we created indexes over the fields that joins happened over, the speed drastically increased. To the point where Mongodb actually was left behind.
However, during development when the datastructure was more fluid and rapidly changing, Mongodb was way easier to work with because it didnt adhere to a schema
Results:

GetBooksWithCityMysql started
Times:
1,799
2,288
0,493
0,944
0,423
Median: 0,944
Average: 1,1894

GetBooksWithCityMongoDB started
Times:
66,17
311,424
62,505
294,247
10,019
Median: 66,17
Average: 148,873

GetCitiesInTitleMysql started
Times:
1,569
0,414
0,434
0,425
0,413
Median: 0,425
Average: 0,651

GetCitiesInTitleMongoDB started
Times:
0,14
0,281
0,755
1,446
0,141
Median: 0,281
Average: 0,5526

GetCitiesWithAuthorMysql started
Times:
0,447
0,666
0,478
0,483
0,445
Median: 0,478
Average: 0,5038

GetCitiesWithAuthorMongoDB started
Times:
0,137
1,274
0,727
0,497
0,136
Median: 0,497
Average: 0,5542

GetBooksMentionedInAreaMysql started
Times:
0,429
0,421
1,555
1,537
1,071
Median: 1,071
Average: 1,0026
