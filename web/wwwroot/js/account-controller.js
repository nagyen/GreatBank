angular.module("app")
.controller("AccountController", ["$scope", "$http", function($scope, $http){
    // init status
    $scope.status = {
        validatationErrors: ""
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
                $scope.status.ValidatationErrors = res.text;
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

        $http.get("/account/GetTransactions?page="+page)
        .success(function(res){
            if(res.code != undefined && res.code != 0)
            {
                $scope.status.ValidatationErrors = res.text;
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
                $scope.status.ValidatationErrors = res.text;
            }
            else
            {
                $scope.model.currentBalance = res;
            }

        });
    }

}]);