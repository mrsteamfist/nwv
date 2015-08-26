function CreateStyleSheet(selector, rules)
{
    var sheet = document.styleSheets[0];

    if ("insertRule" in sheet) {
        sheet.insertRule(selector + "{" + rules + "}", 1);
    }
    else if ("addRule" in sheet) {
        sheet.addRule(selector, rules, 1);
    }
}