function pagination(pageSize, currentIndex, recordsCount) {

    var lastPage, PrePage, nextPage;
    var pagingContent = [];

    var pageCount = recordsCount / pageSize;
    lastPage = pageCount % 1 === 0 ? pageCount : parseInt(pageCount) + 1

    PrePage = currentIndex - 1;
    nextPage = currentIndex + 1;

    if (recordsCount == 0)
        currentIndex = 0;

    if (recordsCount != 0) {
        pagingContent.push("<div class=\"span6\">" +
                    "<div id=\"dt_a_info\" style=\"float:right\" class=\"dataTables_info\">Page " + currentIndex + " of " + lastPage + "</div>" +
                    "</div>");
    }
    pagingContent.push("<div class=\"span6\"><div id=\"pagingDiv\" class=\"dataTables_paginate paging_bootstrap_alt pagination\"><ul>");

    //Set previous index & first index
    if (currentIndex == 0 || currentIndex == 1) {
        pagingContent.push("<li class=\"first disabled\"><span><< First</span></li>",
            "<li class=\"prev disabled\"><span>< Pre</span></li>");
    }
    else {
        pagingContent.push("<li class=\"first\"><a href=\"javascript:indexChanged(1)\"><< First</a></li>",
            "<li class=\"prev\"><a href=\"javascript:indexChanged(" + PrePage + ")\">< Pre</a></li>");
    }

    //Set page numbers
    var startIndex, LastIndex;
    if (lastPage <= 5) {
        startIndex = 1;
        LastIndex = lastPage;
    }
    else if (currentIndex < 5) {
        startIndex = 1;
        LastIndex = 5;
    }
    else if (currentIndex + 2 > lastPage) {
        startIndex = lastPage - 4   ;
        LastIndex = lastPage;
    }
    else {
        startIndex = currentIndex - 2;
        LastIndex = currentIndex + 2;
    }

    for (var i = startIndex; i <= LastIndex; i++) {
        if (i == currentIndex)
            pagingContent.push("<li class=\"active\"><a href=\"javascript:indexChanged(" + i.toString() + ")\">" + i.toString() + "</a></li>");
        else
            pagingContent.push("<li><a href=\"javascript:indexChanged(" + i.toString() + ")\">" + i.toString() + "</a></li>");
    }

    //Set next and last index
    if (currentIndex == lastPage) {
        pagingContent.push("<li class=\"next disabled\"><span>Next ></span></li>",
            "<li class=\"last disabled\"><span>Last >></span></li>");
    }
    else {
        pagingContent.push("<li class=\"next\"><a href=\"javascript:indexChanged(" + nextPage + ")\">Next ></a></li>",
            "<li class=\"last\"><a href=\"javascript:indexChanged(" + lastPage + ")\">Last >></a></li>");
    }

    pagingContent.push("</ul></div></div>");
    return pagingContent.join("");
}