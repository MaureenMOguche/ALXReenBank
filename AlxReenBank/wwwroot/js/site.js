const ctx = document.getElementById('myChart');

new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: ['Cost', 'Income'],
        datasets: [{
            label: 'Amount',
            data: [84000, 300000],
            backgroundColor: [
                'rgb(51, 63, 61)',
                'rgb(117, 200, 162)',
            ],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});


function previewImage(event) {
    const reader = new FileReader();
    reader.onload = function () {
        const element = document.getElementById('previewImage');
        element.src = reader.result;
    }
    reader.onerror = function () {
        const element = document.getElementById('errorMsg');
        element.value = "Couldn't load the image.";
    }
    reader.readAsDataURL(event.target.files[0]);
}

const input = document.getElementById('inputImage');
input.addEventListener('change', (event) => {
    previewImage(event)
});