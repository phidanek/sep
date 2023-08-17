var dataTable;

$(document).ready(function () {
	loadDataTable();
});

function loadDataTable() {
	dataTable = $('#tblData').DataTable({
		"ajax": {
			"url": "/Employer/JobPost/GetAll"
		},
		"columns": [
			{ "data": "jobTitle", "width": "15%" },
			{ "data": "department.name", "width": "15%" },
			{ "data": "jobType.name", "width": "15%" },
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
			{ "data": "status.name", "width": "15%" },

			{
				"data": "id",
				"render": function (data) {
					return `
						<div class="w-75 btn-group" role="group">
						<a href="/Employer/JobPost/Upsert?id=${data}"
						class="btn btn-primary mx-2">Review</a>
					</div>
						`
				},
				"width": "15%"
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