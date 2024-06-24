

document.addEventListener("DOMContentLoaded", () => {
    const commentForm = document.getElementById("comment-form");
    const commentList = document.getElementById("comment-list");

    // Load comments from localStorage
    const loadComments = () => {
        const comments = JSON.parse(localStorage.getItem("comments")) || [];
        comments.forEach(addCommentToDOM);
    };

    // Save comments to localStorage
    const saveComments = (comments) => {
        localStorage.setItem("comments", JSON.stringify(comments));
    };

    // Add a comment to the DOM
    const addCommentToDOM = (comment) => {
        const commentElement = document.createElement("div");
        commentElement.className = "comment-list";
        commentElement.innerHTML = `
            <div class="flex">
                <div class="user">
                    <div class="user-image"><img src="photos/user icon.jpg" alt=""></div>
                    <div class="user-meta">
                        <div class="name fs-3">${comment.name}</div>
                        <div class="day">${timeAgo(comment.timestamp)}</div>
                    </div>
                </div>  
            </div>
            <div class="comment">${comment.text}</div>
            <div class="reply">
                <div class="lick icon"> <i class="fa-regular fa-thumbs-up"></i></div>
                <div class="dislick icon"> <i class="fa-regular fa-thumbs-down"></i> </div>
                <div class="re-comment">Reply..</div>
                <div class="delet">Delete</div>
            </div>
            <hr class="horizental-line">
        `;
        commentList.appendChild(commentElement);

        // Add delete functionality
        commentElement.querySelector(".delet").addEventListener("click", () => {
            deleteComment(comment);
            commentList.removeChild(commentElement);
        });
    };

    // Handle form submission
    commentForm.addEventListener("submit", (e) => {
        e.preventDefault();
        const commentInput = document.getElementById("comment-input");
        const commentText = commentInput.value.trim();

        if (commentText) {
            const comment = {
                name: "USERNAME ", // You can change this to the current user's name
                text: commentText,
                timestamp: Date.now()
            };

            // Save comment to localStorage
            const comments = JSON.parse(localStorage.getItem("comments")) || [];
            comments.push(comment);
            saveComments(comments);

            // Add comment to the DOM
            addCommentToDOM(comment);
            commentInput.value = "";
        }
    });

    // Delete comment from localStorage
    const deleteComment = (commentToDelete) => {
        let comments = JSON.parse(localStorage.getItem("comments")) || [];
        comments = comments.filter(comment => comment.timestamp !== commentToDelete.timestamp);
        saveComments(comments);
    };
    
    // Update comment times dynamically
    const updateCommentTimes = () => {
        const commentElements = document.querySelectorAll(".comment-list .day");
        const comments = JSON.parse(localStorage.getItem("comments")) || [];
        commentElements.forEach((element, index) => {
            element.textContent = timeAgo(comments[index].timestamp);
        });
    };

    // Convert timestamp to time ago format
    const timeAgo = (timestamp) => {
        const now = Date.now();
        const secondsPast = (now - timestamp) / 1000;
        if (secondsPast < 60) {
            return `${Math.round(secondsPast)} seconds ago`;
        } else if (secondsPast < 3600) {
            return `${Math.round(secondsPast / 60)} minutes ago`;
        } else if (secondsPast < 86400) {
            return `${Math.round(secondsPast / 3600)} hours ago`;
        } else {
            return `${Math.round(secondsPast / 86400)} days ago`;
        }
    };

    // Load comments and set interval to update times
    loadComments();
    setInterval(updateCommentTimes, 60000);
});
