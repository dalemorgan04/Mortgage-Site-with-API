//This could have been done by binding the form in Razor
//I wanted to add variety and demo an alternative approach

$(function () {
    mortgage_search_critera.init();
});

var mortgage_search_critera = {
    init: function () {
        $('#feedback').hide();
        $('#loanToValueContainer').hide();
        $(document)
            .on('click', '#search', mortgage_search_critera.search)
            .on('focusout', '#propertyValue', mortgage_search_critera.calculateLoanToValue)
            .on('focusout', '#deposit', mortgage_search_critera.calculateLoanToValue);
    },
    formIsValid: function (e) {
        var propertyValue = $('#propertyValue').val();
        var $feedback = $('#feedback');
        if (propertyValue === '' || isNaN(propertyValue) || propertyValue < 0 || propertyValue > 9999999) {
            $feedback.html("Please enter a valid property value between 0 & 9,999,999");
            $feedback.show();
            return false;
        }
        var deposit = $('#deposit').val();
        if (deposit === '' || isNaN(deposit) || deposit < 0 || deposit > 9999999) {
            $feedback.html("Please enter a valid deposit value between 0 & 9,999,999");
            $feedback.show();
            return false;
        }

        var loanToValue = mortgage_search_critera.calculateLoanToValue();
        if (loanToValue > 0.9 || loanToValue <= 0 ) {
            $feedback.html("Sorry your loan to value ratio disqualifies you");
            $feedback.show();
            return false;
        }
        return true;
    },
    calculateLoanToValue: function () {
        $('#feedback').hide();
        var propertyValue = $('#propertyValue').val();
        var deposit = $('#deposit').val();
        var loanToValue = 0;
        if ((deposit > 0 && deposit < 9999999) &&
            (propertyValue > 0 && propertyValue < 9999999) &&
            (propertyValue - deposit > 0)) {

            loanToValue = (propertyValue - deposit) / propertyValue ;
            $('#loanToValue').empty();
            $('#loanToValue').append( Math.floor(loanToValue * 100).toFixed(0) + '%');
            $('#loanToValueContainer').show();
        } else {
            $('#loanToValueContainer').hide();
        }
        return loanToValue;
    },
    search: function (e) {
        e.preventDefault();
        if (mortgage_search_critera.formIsValid()) {
            var resultsUrl = 'your-search-results';
            var viewModel = {
                CustomerId: $('#customerId').val(),
                PropertyValue: $('#propertyValue').val(),
                Deposit: $('#deposit').val(),
                MortgageType: $('#mortgageType').val()
            };

            $.ajax({
                type: 'POST',
                url: 'Search/CreateSearch',
                contentType: 'application/json',
                data: JSON.stringify(viewModel),
                async: true,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("RequestVerificationToken",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                success: function (data, status, xhr) {
                    window.location.href = resultsUrl + '?id=' + data.searchId;
                },
                error: function (xhRequest, ErrorText, thrownError) {
                    console.log('xhRequest: ' + xhRequest + "\n");
                    console.log('ErrorText: ' + ErrorText + "\n");
                    console.log('thrownError: ' + thrownError + "\n");
                }
            });
        }
    }
}