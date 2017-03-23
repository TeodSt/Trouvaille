function redirect(result) {
    if (result.url) {
        window.location.href = result.url;
    }
}