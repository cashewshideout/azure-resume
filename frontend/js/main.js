window.addEventListener('DOMContentLoaded', (event) => {
    getVisitCount();
});

const functionApi = 'http://localhost:7071/api/AzureResumeCounter';

const getVisitCount = () => {
    let count = 30;
    fetch(functionApi).then(response => {
        console.log(response);
        return response.json()
    }).then(response => {
        console.log(response);
        count = response.count;
        document.getElementById("counter").innerText = count;
        console.log(count);
    }).catch(function(error){
        console.log(error);
    });
    return count;
}