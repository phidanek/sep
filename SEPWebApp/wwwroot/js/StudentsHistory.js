var dataTable;

$(document).ready(function () {
	loadDataTable();
});

function loadDataTable() {
	dataTable = $('#tblData').DataTable({
		"ajax": {
			"url": "/Student/Student/GetAllAppyJobPost"
		},
		"columns": [
			{ "data": "jobPost.jobTitle", "width": "16%" },
			{ "data": "jobPost.department.name", "width": "16%" },
			{ "data": "jobPost.weekHour.name", "width": "12%" },
			{
				"data": "jobPost.startDate",
				"width": "12%",
				"render": function (data) {
					return formatDate(data);
				}
			},
			{
				"data": "jobPost.endDate",
				"width": "12%",
				"render": function (data) {
					return formatDate(data);
				}
			},
			{ "data": "status","width":"12%"},

			{
				"data": "id",
				"render": function (data) {
					return `
						<div class="col" >

							<a href="/Student/Student/ReviewApplication?Id=${data}"
						class="btn btn-primary mx-2">Details</a>

						<a href="/Student/Student/WithdrawApplication?Id=${data}"
						class="btn btn-primary mx-2">Withdraw</a>
						</div>

						`
				},
				"width": "22%"
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
