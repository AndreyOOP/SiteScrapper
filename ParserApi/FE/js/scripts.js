//var hostApi = "https://localhost:44331";
var hostApi = "https://localhost:1234";

function testApiCall() {
    let req = new XMLHttpRequest();
    req.open('GET', hostApi + '/api/parse/a', false);
    req.send();
    alert(req.response);
}