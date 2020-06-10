# HackerNews

Check the following website

https://news.ycombinator.com/

```mermaid
sequenceDiagram
HackerNewsView ->> HackerNewsViewBack: get first html bad formed
HackerNewsView ->> HackerNewsViewBack: get style sheet
HackerNewsView ->> HackerNewsViewBack: get style sheet source images
HackerNewsView ->> HackerNewsViewBack: get javascript "HackerNews' View back"
HackerNewsViewBack ->> HackerNewsView: Reformat dom and create a well formed html
```
