namespace Domain.Common
{
    public enum StatusBenefit
    {
        MessageSentToQueue,
        ProcessingMessage,
        MessageProcessingComplete,
        ErrorSendingMessageToQueue
    }
}
