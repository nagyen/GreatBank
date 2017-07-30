angular.module("app")
.controller("AccountController", ["$scope", "$http", function($scope, $http){
    //debug
    $scope.DEBUG = false;

    // init status
    $scope.status = {
        validationErrors: "",
        alert: ""
    };

    // init model
    $scope.model = {};

    // init controller
    $scope.init = function(){
        $scope.refreshUser();
        $scope.refreshTransactions();
    }

    // function that gets user detials
    $scope.refreshUser = function(){
        $http.get("/account/getUserAccount")
        .success(function(res){
            if(res.code != undefined && res.code != 0)
            {
                $scope.status.validationErrors = res.text;
            }
            else
            {
                $scope.model.user = res;
            }

        });
    }

    // function that gets transaction listing
    $scope.refreshTransactions = function(page){
        if(!page) page = 1;
        $scope.model.transactionsPage = page;
        $http.get("/account/GetTransactions?page="+page)
        .success(function(res){
            if(res.code != undefined && res.code != 0)
            {
                $scope.status.validationErrors = res.text;
            }
            else
            {
                $scope.model.transactions = res;
            }

        });
        $scope.refreshCurrentBalance();
    }

    // get current balance
    $scope.refreshCurrentBalance = function(){
        $http.get("/account/getCurrentBalance")
        .success(function(res){
            if(res.code != undefined && res.code != 0)
            {
                $scope.status.validationErrors = res.text;
            }
            else
            {
                $scope.model.currentBalance = res;
            }

        });
    }

    // record a transaction
    $scope.recordTransaction = function(){
        $scope.status.validationErrors = "";
        var model = $scope.model;
        // get transaction detials
        var transType = model.transactionType;
        var amount = model.transactionAmount;
        // check if valid transaction type and amount
        if(!transType || !amount)
        {
            $scope.status.validationErrors = "Please select valid transaction type and amount.";
            return;
        }

        //  create transaction
        $http.post("/account/"+transType, amount)
        .success(function(res){
            if(res.code != undefined && res.code != 0)
            {
                console.log(res.text);
                $scope.status.validationErrors = res.text;
            }
            else
            {
                $scope.refreshTransactions();
                model.transactionType = "";
                model.transactionAmount = "";
                $scope.status.alert = "Transaction success!";
                $("#alertModel").modal("show");

            }
        })

    }

}]);