import { Pipe, PipeTransform } from '@angular/core';
const padding = "000000";

@Pipe({
    name: 'ntsnumber',
    pure: false
})
export class NtsNumberPipe implements PipeTransform {
    private prefix: string;
    private decimal_separator: string;
    private thousands_separator: string;
    private suffix: string;

    constructor() {
        this.prefix = '';
        this.suffix = '';
        this.decimal_separator = ',';
        this.thousands_separator = '.';
    }

    transform(value: string, fractionSize: number = 0): string {
        if (!value) {
            return value;
        }

        if (parseFloat(value) % 1 != 0) {
            if (value && value.toString().indexOf(".") != -1) {
                let start = value.toString().indexOf(".");
                let count = value.toString().substring(start + 1, value.length);

                fractionSize = count.length;
            } else {
                fractionSize = 6;
            }
        }

        let [integer, fraction = ""] = (parseFloat(value).toString() || "").toString().split(".");

        fraction = fractionSize > 0
            ? this.decimal_separator + (fraction + padding).substring(0, fractionSize) : "";
        integer = integer.replace(/\B(?=(\d{3})+(?!\d))/g, this.thousands_separator);
        if (isNaN(parseFloat(integer))) {
            integer = "0";
        }
        return this.prefix + integer + fraction + this.suffix;

    }

    parse(value: string, fractionSize: number = 0): string {
        let [integer, fraction = ""] = (value || "").replace(this.prefix, "")
            .replace(this.suffix, "")
            .split(this.decimal_separator);

        integer = integer.replace(new RegExp(this.thousands_separator, "g"), "");

        fraction = parseInt(fraction, 10) > 0 && fractionSize > 0
            ? this.decimal_separator + (fraction + padding).substring(0, fractionSize)
            : "";

        return integer + fraction;
    }

    setOptions(options: any) {
        this.prefix = options.prefix;
        this.suffix = options.suffix;
        this.decimal_separator = options.decimal;
        this.thousands_separator = options.thousands;
    }
}
