//var hostApi = "https://localhost:44331";
//var hostApi = "https://localhost:1234";
var hostApi = "https://f2e78241c2fe.ngrok.io";

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
    } else {
        console.log(response.status);
    }
}

function insertHtm(selector, html) {
    var targetElem = document.querySelector(selector);
    targetElem.innerHTML = html;
}