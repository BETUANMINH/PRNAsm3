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
        .then(data => {
            let html = data.map(item => {
                let createdDate = new Date(item.CreatedDate).toLocaleDateString('en-US', {
                    year: 'numeric',
                    month: 'short',
                    day: 'numeric'
                });
                let updatedDate = new Date(item.UpdatedDate).toLocaleDateString('en-US', {
                    year: 'numeric',
                    month: 'short',
                    day: 'numeric'
                });
                let PublishStatus = item.PublishStatus == 1 ? "Published" : "Draft";
                return `<tr> 
                    <td>${createdDate}</td>
                    <td>${updatedDate}</td>
                    <td>${item.Title}</td>
                    <td>${item.Content}</td>
                    <td>${PublishStatus}</td>
                    <td>${item.AuthorID}</td>
                    <td>${item.CategoryID}</td>
                    <td>
                        <a href='/Posts/Edit?id=${item.PostID}'>Edit</a>
                        <a href='/Posts/Delete?id=${item.PostID}'>Delete</a>
                    </td>
                </tr>`;
            }).join('');
            tbody.innerHTML = html;
        })
        .catch(error => console.error('Error fetching posts:', error));
}
