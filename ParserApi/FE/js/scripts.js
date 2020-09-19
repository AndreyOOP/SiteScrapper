var hostApi = "https://localhost:44331";
//var hostApi = "https://localhost:1234";
//var hostApi = "https://f2e78241c2fe.ngrok.io";

async function parse(id) {

    var uri = hostApi + '/api/parse/' + id;

    let response = await fetch(uri,
        {
            method: 'post',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify({ showLog: false })
        });

    if (response.ok) {
        let txtResponse = await response.text();

        insertHtm("#search_result", txtResponse);
        insertHtm("#tbl_upd", createTable(txtResponse));
    } else {
        console.log(response.status);
    }
}

// ToDo: refactor => createTable(name, rows)
function createTable(jsonText) {

    var json = JSON.parse(jsonText);

    var table = "";
    for (var i = 0; i < json.Main.length; i++) {
        var x = json.Main[i];
        var row = "<tr>" +
            "<td>" + x.ParserName + "</td>" +
            "<td>" + x.Brand + "</td>" +
            "<td>" + x.Name + "</td>" +
            "<td>" + x.Price + "</td>" +
            "<td>" + "<a href='" + x.LinkToSource + "'>Link</a>" + "</td>" +
            "</tr>";
        table += row;
    }
    return table;
}

function insertHtm(selector, html) {
    var targetElem = document.querySelector(selector);
    targetElem.innerHTML = html;
}