angular.module("app")
.controller("HomeController", ["$scope", "$http", function($scope, $http){
    // init status
    $scope.status = {
        Register: false,
        ValidatationErrors: ""
    };
    // init model
    $scope.model = {};

    // function to login user
    $scope.login = function(){
        var model = $scope.model;
        console.log($scope.frm.$valid);
        //check if form valid
        if($scope.frm.$valid) {

            // login
            $http.post("/home/login", model)
            .success(function(res) {
                console.log("response");
            })

        }
        else {
            $scope.status.ValidatationErrors = "Please fill all required fields."
        }
    }

    // switch screen
    $scope.switchScreen = function(registerScreen) {
        $scope.status.Register = registerScreen;
        $scope.status.ValidatationErrors = "";
    }
}]);