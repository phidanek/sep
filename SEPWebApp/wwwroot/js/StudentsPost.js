var dataTable;

$(document).ready(function () {
	loadDataTable();
});

function loadDataTable() {
	dataTable = $('#tblData').DataTable({
		"ajax": {
			"url": "/Student/Student/GetAllJobpost"
		},
		"columns": [
			{ "data": "jobTitle", "width": "12%" },
			{ "data": "department.name", "width": "12%" },
			{ "data": "weekHour.name", "width": "12%" },
			{
				"data": "startDate",
				"width": "12%",
				"render": function (data) {
					return formatDate(data);
				}
			},
			{
				"data": "endDate",
				"width": "12%",
				"render": function (data) {
					return formatDate(data);
				}
			},
			{ "data": "hourlyRate","width":"12%"},
			{
				"data": "closingDate",
				"width": "12%",
				"render": function (data) {
					return formatDate(data);
				}
			},

			{
				"data": "id",
				"render": function (data) {
					return `
						<div class="w-75 btn-group" role="group">
						<a href="/Student/Apply?id=${data}"
						class="btn btn-primary mx-2">Apply</a>
					</div>
						`
				},
				"width": "12%"
			}

		]
	});
}
function formatDate(date) {
	var formattedDate = new Date(date).toLocaleDateString('en-US', {

		year: 'numeric',
		month: '2-digit',
		day: '2-digit'
	});
	return formattedDate;
}