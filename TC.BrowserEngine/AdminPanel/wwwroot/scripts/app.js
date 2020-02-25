angular.module('app', ['ngRoute', 'components'])
  .config(function ($routeProvider) {
    $routeProvider
      .when("/", {
        templateUrl: "main.html",
        controller: "mainCtrl"
      })
      .when("/login", {
        templateUrl: "login.html",
        controller: "loginCtrl"
      })
      .when("/green", {
        templateUrl: "green.htm"
      })
      .when("/blue", {
        templateUrl: "blue.htm"
      });
  })
  .controller('mainCtrl', function ($scope, $http, $location) {
    $scope.logoutClick = function () {
      $http.post('/logout').then(function () {
        $location.path("/login");
      }, function (error) {        
      });
    } 
  }).controller('loginCtrl', function ($scope, $http, $location) {
    $scope.submit = function () {
      var data = {
        email: $scope.email,
        password: $scope.password
      }
      $http.post('/login', data).then(function () {
        $location.path("/");
      }, function (error) {
          if (error.data.status == 401) {
            $scope.error = "Email or Password is incorrect";
          }
        
      });
    }
  });