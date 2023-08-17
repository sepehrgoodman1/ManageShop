﻿using Taav.Contracts.Interfaces;

namespace ManageShop.Infrastucture.Queries;

class PageResult<T> : IPageResult<T>
    where T : class
{
    public IEnumerable<T> Elements { get; init; }
    public int TotalElements { get; init; }

    public PageResult(IEnumerable<T> elements, int totalElements)
    {
        Elements = elements;
        TotalElements = totalElements;
    }
}