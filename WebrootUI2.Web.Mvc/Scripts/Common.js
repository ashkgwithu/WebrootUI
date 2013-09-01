// Date functions
function convertDate(str) {
    var m = str.match(/(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})(\.\d{2,3})?Z/);
    var date = new Date();
    if (m) {
        date.setUTCFullYear(parseInt(m[1]), parseInt(m[2]) - 1, parseInt(m[3]));
        date.setUTCHours(parseInt(m[4]), parseInt(m[5]), m.length > 4 ? parseInt(m[6]) : 0);
        return pad(date.getMonth() + 1) + '/'
            + pad(date.getDate()) + '/'
            + pad(date.getFullYear()) + ' '
            + pad(date.getHours()) + ':'
            + pad(date.getMinutes()) + ':'
            + pad(date.getSeconds());
    }
    return str;
}

// Common functions
function pad(n) {
    return n < 10 ? '0' + n : n;
}

function escapeHtml(text) {
    return text.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
}