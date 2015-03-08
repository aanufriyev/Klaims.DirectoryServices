(function() {
	"use strict";

	var MainController = function($scope) {
		$scope.message = "dummy message to check everything in place";
	};

	angular.module("klaims").controller("MainController", ["$scope", MainController]);
})();