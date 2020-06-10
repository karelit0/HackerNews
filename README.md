# HackerNews

Check the following website

https://news.ycombinator.com/

## Background

> This website is very interesting on how it display information, and how it does render the dom.

1. The Hacker news view request the first DOM that is a html bad formed, with a lot of errors inside
2. The Hacker news view request one style sheet
3. The Hacker news view request all the style sheet source images
4. The Hacker news view request one javascript "HackerNews' View back"
5. The HackerNews' View back do a refactor of the DOM, to build a well formed html

## TODO

>+ Undertand where the information is on the first DOM that website respond
>+ Create one functionallity to read that information with out using HtmlAgile Pack and/or any other library to sanitaze the html
>+ Create one functionallity to read that information using HtmlAgile Pack and HtmlSanitizer libraries to sanitaze and parse the html
>+ Create WebApi to request json
>+ Create custom filters using LambdaExpressionBuilder library
>+ Expose endpoints information throw Swagger

## Final Work
> Web Api Description
![](https://github.com/karelit0/HackerNews/blob/master/documentation/images/webapi.description.png)

> HtmlSanitizer Endpoint Request using no filters
![](https://github.com/karelit0/HackerNews/blob/master/documentation/images/Endpoint.HtmlSanitizer.empty.request.png)
> Response
https://github.com/karelit0/HackerNews/blob/master/documentation/images/Endpoint.HtmlSanitizer.empty.response.json

> HtmlSanitizer Endpoint Request using filters
![](https://github.com/karelit0/HackerNews/blob/master/documentation/images/Endpoint.HtmlSanitizer.filtered.request.png)
> Response
https://github.com/karelit0/HackerNews/blob/master/documentation/images/Endpoint.HtmlSanitizer.filtered.response.json

> ManualCleaner Endpoint Request using no filters
![](https://github.com/karelit0/HackerNews/blob/master/documentation/images/Endpoint.ManualCleaner.empty.request.png)
> Response
https://github.com/karelit0/HackerNews/blob/master/documentation/images/Endpoint.ManualCleaner.empty.response.json

> ManualCleaner Endpoint Request using filters
![](https://github.com/karelit0/HackerNews/blob/master/documentation/images/Endpoint.ManualCleaner.filtered.request.png)
> Response
https://github.com/karelit0/HackerNews/blob/master/documentation/images/Endpoint.ManualCleaner.filtered.response.json
