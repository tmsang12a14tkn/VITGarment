String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

jQuery('#datepicker').datetimepicker(
    {
        timepicker: false,
        format: 'd/m/Y',
        value: date,
        onChangeDateTime: function (dp, $i) {
            window.location = "/Goals/FactoryCreate?factoryId=" + factoryId + "&date=" + $i.val();
        }
    });
$("#btndatepicker").on("click", function () {
    $('#datepicker').datetimepicker('toggle');
});

$('#editQCReportModal').on('hidden.bs.modal', function (e) {
    $(this).find('.modal-content').empty();
})
$('#editQCReportModal').on('show.bs.modal', function (e) {
    $(this).find('.modal-content').load($(e.relatedTarget).data('href'), onShowEditQCReportModal);
})

var onSuccessEditQCReport = function (data)
{
    if (data.success) {
        $('#editQCReportModal').modal('hide');
        $.get('/QCReport/_QCReportListForTeam?teamId=' + data.teamId + "&date=" + data.date, function (partial)
        {
            $('#qcTeamList-' + data.teamId).replaceWith(partial);
        });
       
    }
}
var onShowEditQCReportModal = function ()
{
    //register events
    $("#ProductCount, #FailProductCount").on("keyup", function () {
        var productCount = $("#ProductCount").val();
        var failProductCount = $("#FailProductCount").val();
        $("#GoodProductCount").val(productCount - failProductCount);
        $("#PercentFail").val((productCount != 0 ? failProductCount * 100 / productCount : 0) + '%');
        
    });
    $("#addErrorRowBtn").on("click", function () {
        var cIndex = $(this).data('index');
        $(this).data('index', cIndex + 1);
        $row = $('<tr></tr>').attr('id', 'error-' + cIndex).html($("#addErrorRow").html());
        $row.insertBefore($("#addErrorRow"));
        $row.find('input').each(function (index, element) {
            $(element).attr('name', $(element).attr('name').replace("Errors[]", "Errors[" + cIndex + "]"));
        });
        $row.find('button').data('id', cIndex);
    });
    $("#addSpecRowBtn").on("click", function () {
        var sIndex = $(this).data('index');
        $(this).data('index', sIndex + 1);
        $row = $('<tr></tr>').attr('id', 'spec-' + sIndex + '-0').html($("#addSpecRow").html().replaceAll("{specIndex}", sIndex));
        $body = $("<tbody></tbody>").attr('id', 'spec-' + sIndex).append($row);
        $body.insertBefore($("#tempalate-addSpec"));
        //$row.find('input').each(function (index, element) {
        //    $(element).attr('name', $(element).attr('name').replace("Specifications[]", "Specifications[" + sIndex + "]").replace("{specIndex}", sIndex));
        //});
        //$row.find('button').each(function (index, element) {
        //    $(element).attr('data-index', $(element).attr('data-index').replace("{specIndex}", sIndex));
        //});
        $row.find('button.btn-deleteSpecDetail').data('index', sIndex).data('dindex', '0');
    });

    $("#table-error").on('click', '.btn-deleteError', function () {
        var errorid = $(this).data('id');
        $('#error-' + errorid).find('.IsDeletedError').val('true');
        $('#error-' + errorid).addClass('hidden');

        return false;
    });

    $("#table-spec").on('click', '.btn-deleteSpecDetail', function () {
        var sIndex = $(this).data('index');
        var dIndex = $(this).data('dindex');
        var specDetailId = sIndex + '-' + dIndex;
        var $thisDetail = $('#spec-' + specDetailId);
        $thisDetail.find('.IsDeletedSpecDetail').val('true');

        var $spanables = $thisDetail.find('.spanable');
        if ($spanables.length == 0) {
            $thisDetail.addClass('hidden');
        }
        else
        {
            var $otherDetails = $('#spec-' + sIndex).find('tr').not('.hidden').not('#spec-' + specDetailId);
            if ($otherDetails.length == 0) {
                $thisDetail.addClass('hidden');
                $('#spec-' + sIndex).addClass('hidden');
                $('#spec-' + sIndex).find('.IsDeletedSpec').val('true');
            }
            else
            {
                var $firstOther = $otherDetails.first();
                $firstOther.prepend($spanables.first());
                $firstOther.append($spanables.last());
                $thisDetail.addClass('hidden');
            }
        }
        return false;
    });

    $("#table-spec").on('click', '.btn-addSpecDetail', function () {
        var sIndex = $(this).data('index');
        var dIndex = $(this).data('dindex');
        dIndex++;
        $(this).data('dindex', dIndex);
        $row = $('<tr></tr>').attr('id', 'spec-' + sIndex + "-" + dIndex).html($("#addSpecDetailRow").html().replaceAll("{specIndex}", sIndex).replaceAll("{detailIndex}", dIndex));
        $row.appendTo($("#spec-" + sIndex));
        $("#spec-" + sIndex + " td.spanable").attr("rowspan", dIndex + 1);
        //$row.find('input').each(function (index, element) {
        //    $(element).attr('name', $(element).attr('name'));
        //});
        $row.find('button.btn-deleteSpecDetail').data('index', sIndex).data('dindex', dIndex);

        return false;
    })
}