window.exampleJsFunctions = {
    showPrompt: function (text) {
        return prompt(id, 'Task is id 10101');
    },
    displayDescription: function (descriptionMessage) {
        document.getElementById('welcome').innerText = welcomeMesage;
    },
    returnArrayAsyncJs: function () {
        DotNet.invokeMethodAsync('Blazor Sample', 'ReturntaskAsyc')
            .then(data => {
                data.push(4);
                console.log(data);
            });
    },
    sayDisplay: function (dotnetHelper) {
        return dotnetHelper.invokeMethodAsync('displayTask')
            .then(r => console.log(r));
    }
};
