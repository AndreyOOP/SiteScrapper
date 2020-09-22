//var hostApi = "https://localhost:44331";
//var hostApi = "https://localhost:1234";
//var hostApi = "https://f2e78241c2fe.ngrok.io";
var hostApi = "http://devpartsparser.azurewebsites.net";

async function parse(id) {

    var uri = hostApi + '/api/parse/' + id;

    let response = await fetch(uri,
        {
            method: 'post',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify({ showLog: false })
        });

    if (response.ok) {

        let json = await response.json();

        var partsTable = generateTable(json.Parts, "Parts Table", ["ParserName", "Brand", "Name", "Price", "LinkToSource"]);
        insertHtm("#parts_table_id", partsTable);

        var perRequestPartsTable = generateTable(json.PerRequestParts, "Available Per Request Parts", ["ParserName", "PartId", "Brand", "Name", "Qty", "PriceUah", "LinkToSource"]);
        insertHtm("#per_request_parts_table_id", perRequestPartsTable);

        insertHtm(
            "#analog_parts_table_id",
            generateTable(json.AnalogParts, "Analogs Table", ["ParserName", "Name", "Price", "LinkToSource"])
        );

        insertHtm(
            "#matching_parts_table_id",
            generateTable(json.MatchingParts, "Same Part Name Table", ["ParserName", "Name", "Price", "LinkToSource"])
        );

        insertHtm("#search_result", json.Json);

    } else {
        console.log(response.status);
    }
}

function generateTable(data, caption, names) {

    if (data === null)
        return "";

    var captionElement = "<caption>" + caption + "</caption>";

    var ths = "";
    for (var k = 0; k < names.length; k++) {
        ths += "<th>" + names[k] + "</th>";
    }
    var tableHead = "<thead><tr>" + ths + "</tr></thead>";

    var dataRows = "";
    for (var i = 0; i < data.length; i++) {

        var row = data[i];

        var singleRow = "<tr>";
        for (var j = 0; j < names.length; j++) {
            singleRow += "<td>" + row[names[j]] + "</td>";
        }
        singleRow += "</tr>";

        dataRows += singleRow;
    }
    var tableBody = "<tbody>" + dataRows + "</tbody>";

    return captionElement + tableHead + tableBody;
}

function insertHtm(selector, html) {
    var targetElem = document.querySelector(selector);
    targetElem.innerHTML = html;
}