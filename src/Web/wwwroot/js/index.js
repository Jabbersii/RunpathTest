var initDataTable = function (requestUrl) {
    $('#photoTable').DataTable({
        ajax: {
            url: requestUrl,
            dataSrc: ''
        },
        columns: [
            { data: 'photoTitle' },
            { data: 'albumName' },
            {
                data: 'thumbnailUrl',
                render: function (data, type, row) {
                    return '<a href="' + row.url + '">' +
                        '<img src="' + data + '"/>' +
                        '</a>';
                },
                orderable: false,
                searchable: false
            }
        ]
    });
}