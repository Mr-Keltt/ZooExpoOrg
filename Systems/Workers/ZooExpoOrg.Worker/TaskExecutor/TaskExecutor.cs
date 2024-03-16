using ZooExpoOrg.Services.Logger;
using ZooExpoOrg.Services.RabbitMq;

namespace ZooExpoOrg.Worker;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger logger;
    private readonly IRabbitMq rabbitMq;

    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq
    )
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
    }

    public void Start()
    {
        
    }
}
