var connection = new signalR.HubConnectionBuilder().withUrl("/signalrhub").build();
connection.start().catch(err => alert(err));

connection.on("LoadPosts", function () {
    ShowPost();
});
let page = document.getElementById('currentPage');
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('searchInput').addEventListener('keydown', function (event) {
        if (event.key === 'Enter') {
            event.preventDefault();
            document.getElementById('searchbtn').click();
        }
    });

    ShowPost();
});

function Filter() {
    document.getElementById('currentPage').value = 1; // Reset to first page on search
    ShowPost();
}

function previousPage() {
    let currentPage = parseInt(document.getElementById('currentPage').value);
    if (currentPage > 1) {
        document.getElementById('currentPage').value = currentPage - 1;
        ShowPost();
    }
}

function nextPage() {
    let currentPage = parseInt(document.getElementById('currentPage').value);
    document.getElementById('currentPage').value = currentPage + 1;
    ShowPost();
}

function ShowPost() {
    let currentPage = parseInt(document.getElementById('currentPage').value);
    let searchQuery = document.getElementById('searchInput').value;
    let tbody = document.querySelector("tbody");
    tbody.innerHTML = "";

    fetch(`/Posts/Index?handler=GetPosts&pageNumber=${currentPage}&query=${encodeURIComponent(searchQuery)}`)
        .then(res => res.json())
        .then(data => {
            if (!data || !Array.isArray(data.items)) {
                throw new Error('Invalid data returned');
            }

            let html = data.items.map(item => {
                let createdDate = new Date(item.createdDate).toLocaleDateString('en-US', {
                    year: 'numeric',
                    month: 'short',
                    day: 'numeric'
                });
                let updatedDate = new Date(item.updatedDate).toLocaleDateString('en-US', {
                    year: 'numeric',
                    month: 'short',
                    day: 'numeric'
                });
                let publishStatus = item.publishStatus == 1 ? "Published" : "Draft";
                return `<tr>
                            <td>${item.postID}</td>
                            <td>${createdDate}</td>
                            <td>${updatedDate}</td>
                            <td>${item.title}</td>
                            <td>${item.content}</td>
                            <td>${publishStatus}</td>
                            <td>${item.authorID}</td>
                            <td>${item.categoryID}</td>
                            <td>${item.category?.categoryName || ''}</td>
                            <td>${item.category?.description || ''}</td>
                            <td>
                                <a href='/Posts/Edit?id=${item.postID}'>Edit</a>
                                <a href='/Posts/Delete?id=${item.postID}'>Delete</a>
                                <a href='/Posts/Details?id=${item.postID}'>Details</a>
                            </td>
                        </tr>`;
            }).join('');
            tbody.innerHTML = html;

            document.getElementById('pageInfo').textContent = `Page ${currentPage} / ${data.totalPages}`;
            // Disable/Enable Previous button
            document.getElementById('prevPage').disabled = (currentPage === 1);

            // Disable/Enable Next button
            document.getElementById('nextPage').disabled = (currentPage === data.totalPages);
        })
        .catch(error => console.error('Error fetching posts:', error));
}