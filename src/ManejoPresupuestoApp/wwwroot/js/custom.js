async function deleteCategory(id, controller) {
    var postUrl = `/${controller}/Delete`;
    var redirectUrl = `/${controller}/Index`;

    var response = await fetch(postUrl, {
        method: 'POST',
        body: id,
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const json = await response.json();
    if (!json.success) {
        $("#message").text(json.message);
        const modalError = new bootstrap.Modal(document.getElementById("errorModal"));
        modalError.show();
    }
    else {
        location.href = redirectUrl;
    }
}