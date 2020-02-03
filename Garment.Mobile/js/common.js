Number.prototype.padLeft = function (base, chr) {
    var len = (String(base || 10).length - String(this).length) + 1;
    return len > 0 ? new Array(len).join(chr || '0') + this : this;
}
var dateFromString = function (str) {
    var parts = str.split("/");
    var dt = new Date(parseInt(parts[2], 10),
                      parseInt(parts[1], 10) - 1,
                      parseInt(parts[0], 10));
    return dt;
}
Date.prototype.toString = function () {
    var dateStr = [this.getDate().padLeft(),
               (this.getMonth() + 1).padLeft(),
                this.getFullYear()].join('/');
    return dateStr;
}
Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}
Date.prototype.addMonths = function (months) {
    var dat = new Date(this.valueOf());
    dat.setMonth(dat.getMonth() + months);
    return dat;
}

Number.prototype.toHourString = function () {
    var totalMinutes = this * 60;
    var hour = Math.floor(totalMinutes / 60);
    var minutes = totalMinutes % 60;
    return hour + "h" + Math.round(minutes);
}
String.prototype.hourToNumber = function () {
    var parts = this.split("h");
    var hour = parseInt(parts[0], 10) + parseInt(parts[1], 10) / 60;
    return hour;
}
Date.prototype.dayOfWeek = function () {
    var dows = ["Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"];
    return dows[this.getDay()];
}