ShowPost();
var connection = new signalR.HubConnectionBuilder().withUrl("/signalrhub").build();
connection.start().catch(err => alert(err));

connection.on("LoadPosts", function () {
    ShowPost();
});

function ShowPost() {
    let tbody = document.querySelector("tbody");
    tbody.innerHTML = "";

    fetch("/Posts/Index?handler=GetPosts")
        .then(res => res.json())
        .then(data => data.forEach(item => {
            console.log(item);
            let html = `<tr> 
                    <td>${item.CreatedDate}</td>
                    <td>${item.UpdatedDate}</td>
                    <td>${item.Title}</td>
                    <td>${item.Content}</td>
                    <td>${item.PublishStatus}</td>
                    <td>${item.AuthorID}</td>
                    <td>${item.CategoryID}</td>
                    <td>
                        <a href='/Posts/Edit?id=${item.PostID}'>Edit</a>
                        <a href='/Posts/Delete?id=${item.PostID}'>Delete</a>
                    </td>
                </tr>
                `
            tbody.innerHTML += html;
        }))

}