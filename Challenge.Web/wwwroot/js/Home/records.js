$(document).ready(function () {
    $('#getDataButton').on('click', function () {
        $.ajax({
            type: "GET",
            url: "http://localhost:9001/api/records/list/",
            success: function (data) {
                $('#insertPoint').html(setDataTable(data));
            }
        });
    });
});

function setDataTable(dataReceived) {
    var html = '<table class="table text-left mt-5"><thead><tr><th>Id</th><th>Name</th><th>Status</th></tr></thead><tbody>';
    $.each(dataReceived, function (innerCounter, dataItem) {
        html += `<tr><td>${dataItem.id}</td><td>${dataItem.name}</td><td>${dataItem.status}</td></tr>`;
    });
    html += '</tbody></table>';
    return html;
};