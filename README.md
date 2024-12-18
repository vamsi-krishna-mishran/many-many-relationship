﻿
# Many-Many Relationship in .NET Core Web API.



## Authors

- [@vamsi krishna](https://github.com/vamsi-krishna-mishran)


## Tech Stack


**Server:** .NET Core Web API, EFCore.


## Problem Statement.

 - You are going to have a multiple posts and tags.
 - A post can have multiple tags and vice-versa.
 - You need to create following RestAPIs using EFCore.
 - **Implement CRUD Endpoints**:
   - Design the following API endpoints to manage `Project` and `Task` resources:

     - **Post Endpoints**:
       - `GET /api/posts`: Retrieve all posts, including associated tags.
       - `GET /api/posts/{id}`: Retrieve a specific post by ID, with its associated tags.
       - `POST /api/posts`: Create a new post.
       - `PUT /api/posts/{id}`: Update an existing post.
       - `DELETE /api/posts/{id}`: Delete a post, ensuring any associations with tags are handled appropriately.

     - **Task Endpoints**:
       - `GET /api/tag`: Retrieve all tags.
       - `GET /api/tag/{id}`: Retrieve a specific tag by ID.
       - `POST /api/tag`: Create a new tag.
       - `PUT /api/tag/{id}`: Update an existing tag.
       - `DELETE /api/tag/{id}`: Delete a tag, ensuring any associations with projects are handled appropriately.

     - **Post-Tag Association Endpoints**:
       - `POST /api/post/{postId}/tag/{tagId}`: Associate a task with a post.
       - `DELETE /api/posts/{postId}/tag/{tagId}`: Remove the association between a task and a post.

*Note: Here the main point is to focus on how to handle many to many mapping using EFCore and how to join them to retrieve posts with tags. Remaining endpoints are very simple to do. Any developer can do that easily. mainly focus at GET /posts and Models folder.*






## API Reference

#### Get all posts

```http
  GET /api/post
```



#### Post a Post

```http
  POST /api/post
```

| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| PostDTO      | `JSON` | **Required** |

#### Associate a task to project

```http
  POST /api/post/{projectId}/tasks/{taskId}
```

| parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| projectId      | int | **Required** |
| taskId      | int | **Required** |


#### Unassociate a task to project

```http
  POST /api/post/{projectId}/tasks/{taskId}
```

| parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| projectId      | int | **Required** |
| taskId      | int | **Required** |


#### Delete project

```http
  DELETE /api/post/{projectId}
```

| parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| projectId      | int | **Required** |



#### Get all Tags

```http
  GET /api/tag
```

#### Add a Tag

```http
  POST /api/tag
```

| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| TagDTO      | `JSON` | **Required** |





## Environment Variables

To run this project, you will need to add the following section to your appsettings.json file

"Database":
 {
  "Default": "server=localhost;uid=root;pwd=rootpwd;    database=PostTag"
} 




## Run Locally

Clone the project

```bash
  git clone https://github.com/vamsi-krishna-mishran/many-many-relationship.git
```

Go to the project directory

```bash
  cd many-many-relationship
```

Install dependencies

```bash
  dotnet restore
```

Start the server

```bash
  dotnet run
```


## Support

For support, email vamsiikrishna.dev@gmail.com.

