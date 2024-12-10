# VodafoneTask
I Created .net core Mvc Project with frontend to let you test all features with UI
# Tools Needed 
(Visual Studio, .net 8)
# to run the project
you have to change the connection string in appsetting file then update database using command update-datebase in package manager console
# Project Structure 
  Solution has 4 Projects in it
    1-Domain (Contains Database Entity).
    2-Infrastructure (Contains Mapping,ModelViews,DatabaseOperations Service).
    3-Services (All Project Services).
    4-VodafoneTask(# Main Project).

#(Vodafone Task) Main Part in the Solution
* has controllers, View, and every view has 2 files one for css styling and the other for JavaScript both 2 files have the same name as view
* Every Controller has service, views folder,ModelView folder, css file, JavaScript file with the same name

#Example Api
* Api: http://localhost:5236/Task/GetPaggedDataFilter

* Response: 
{"recordsTotal":1000,"recordsFiltered":7,"data":[{"id":1,"title":"Amr Khaled Mohamed 2","description":"updated","startDate":"2024-12-26T00:00:00","dueDate":"2024-12-18T00:00:00","completionDate":"2024-12-26T00:00:00","status":2,"isDeleted":false,"deletedDate":null},{"id":2,"title":"test1","description":"                    aasas","startDate":"2024-12-12T00:00:00","dueDate":"2024-12-24T00:00:00","completionDate":"2024-12-26T00:00:00","status":1,"isDeleted":false,"deletedDate":null},{"id":3,"title":"test1","description":"                    aaaa","startDate":"2024-12-12T00:00:00","dueDate":"2024-12-24T00:00:00","completionDate":"2024-12-26T00:00:00","status":1,"isDeleted":false,"deletedDate":null},{"id":4,"title":"Amr Shaheen","description":"                    assa","startDate":"2024-12-26T00:00:00","dueDate":"2024-12-28T00:00:00","completionDate":"2024-12-30T00:00:00","status":1,"isDeleted":false,"deletedDate":null},{"id":5,"title":"Amr Khaled Mohamed","description":"                    sdasdasd","startDate":"2024-12-03T00:00:00","dueDate":"2024-12-02T00:00:00","completionDate":"2024-12-05T00:00:00","status":1,"isDeleted":false,"deletedDate":null},{"id":6,"title":"Amr Shaheen","description":"                    sdasdasd","startDate":"2024-12-03T00:00:00","dueDate":"2025-01-10T00:00:00","completionDate":"2024-11-06T00:00:00","status":2,"isDeleted":false,"deletedDate":null},{"id":7,"title":"new task","description":"hey hey","startDate":"2025-01-08T00:00:00","dueDate":"2024-12-04T00:00:00","completionDate":"2025-01-11T00:00:00","status":3,"isDeleted":false,"deletedDate":null}]}
