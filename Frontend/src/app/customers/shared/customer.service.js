"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var environment_1 = require("../../../environments/environment");
require("rxjs/add/operator/map");
var url = environment_1.environment.ApiEndPoint + '/customers';
var CustomerService = (function () {
    function CustomerService(http) {
        this.http = http;
    }
    CustomerService.prototype.getCustomers = function () {
        return this.http
            .get(url);
    };
    CustomerService.prototype.getCustomerById = function (id) {
        return this.http.get(url + '/' + id);
    };
    CustomerService.prototype.createCustomer = function (cust) {
        return this.http.post(url, cust);
    };
    CustomerService.prototype.updateCustomerById = function (id, cust) {
        return this.http.put(url + '/' + id, cust);
    };
    CustomerService.prototype.deleteCustomerById = function (id) {
        return this.http.delete(url + '/' + id);
    };
    CustomerService.prototype.searchQuery = function (q) {
        return this.http.get(url + '/search?q=' + q);
    };
    CustomerService.prototype.setTab = function (tab) {
        this.tab = tab;
    };
    CustomerService.prototype.getTab = function () {
        return this.tab;
    };
    return CustomerService;
}());
CustomerService = __decorate([
    core_1.Injectable()
], CustomerService);
exports.CustomerService = CustomerService;
